using McMaster.Extensions.CommandLineUtils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Resend.DocsCheck;

/// <summary />
public class Program
{
    /// <summary />
    public static int Main( string[] args )
    {
        /*
         * 
         */
        var app = new CommandLineApplication<Program>();

        var svc = new ServiceCollection();

        var sp = svc.BuildServiceProvider();


        /*
         * 
         */
        try
        {
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection( sp );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( "ftl: unhandled exception during setup" );
            Console.WriteLine( ex.ToString() );

            return 2;
        }


        /*
         * 
         */
        try
        {
            return app.Execute( args );
        }
        catch ( UnrecognizedCommandParsingException ex )
        {
            Console.WriteLine( "err: " + ex.Message );

            return 2;
        }
        catch ( Exception ex )
        {
            Console.WriteLine( "ftl: unhandled exception during execution" );
            Console.WriteLine( ex.ToString() );

            return 2;
        }
    }


    /// <summary />
    [Option( "-r|--root", Description = "" )]
    [DirectoryExists]
    [Required]
    public string? RootFolder { get; set; }


    /// <summary />
    public async Task<int> OnExecute()
    {
        var dir = new DirectoryInfo( this.RootFolder! );

        var ok = 0;
        var warn = 0;
        var fail = 0;

        foreach ( var f in dir.GetFiles( "*.mdx", SearchOption.AllDirectories ) )
        {
            var fr = await FileCheck( dir, f );

            ok += fr.Ok;
            warn += fr.Warn;
            fail += fr.Fail;
        }


        /*
         * 
         */
        Console.WriteLine( "{0} ok, {1} warn, {2} fail, {3} total", ok, warn, fail, ok + warn + fail );


        /*
         * 
         */
        if ( fail == 0 )
            return 0;

        return 1;
    }


    /// <summary />
    private async Task<( int Ok, int Warn, int Fail )> FileCheck( DirectoryInfo root, FileInfo f )
    {
        var rel = GetRelativePath( root, f );
        var mdx = await File.ReadAllLinesAsync( f.FullName );


        /*
         * 
         */
        var blocks = new Dictionary<int, string>();

        var sb = new StringBuilder();
        int? blockStart = null;

        foreach ( var line in mdx.Select( ( x, i ) => (Index: i, Content: x) ) )
        {
            if ( blockStart.HasValue == true )
            {
                if ( line.Content.StartsWith( "```" ) == true )
                {
                    blocks.Add( blockStart.Value, sb.ToString() );

                    sb.Clear();
                    blockStart = null;
                }
                else
                {
                    sb.AppendLine( line.Content );
                }

                continue;
            }

            if ( line.Content.StartsWith( "```csharp .NET" ) == true )
            {
                blockStart = line.Index;

                continue;
            }
        }


        /*
         * 
         */
        var ok = 0;
        var wrn = 0;
        var err = 0;

        foreach ( var b in blocks )
        {
            var r = BlockCheck( rel, b.Key, b.Value );

            if ( r == Result.Ok )
                ok++;
            else if ( r == Result.Warn )
                wrn++;
            else
                err++;
        }

        return (ok, wrn, err);
    }


    /// <summary />
    private Result BlockCheck( string rel, int line, string block )
    {
        /*
         * 
         */
        var lines = block.Split( "\n" );

        if ( lines[ 0 ].StartsWith( "// " ) == true )
        {
            var fg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write( "wrn" );
            Console.ForegroundColor = fg;

            Console.WriteLine( " {0} - missing code sample", rel );

            return Result.Warn;
        }


        /*
         * 
         */
        var sb = new StringBuilder();
        sb.AppendLine( "using System;" );
        sb.AppendLine( "using System.Collections.Generic;" );
        sb.AppendLine( "using System.Threading.Tasks;" );

        foreach ( var l in lines )
        {
            if ( l.StartsWith( "using " ) == true )
                sb.AppendLine( l );
        }

        sb.AppendLine();
        sb.AppendLine( "public class Program {" );
        sb.AppendLine( "public static async Task<int> Main( string[] args ) {" );

        foreach ( var l in lines )
        {
            if ( l.StartsWith( "using " ) == true )
                continue;

            sb.AppendLine( l );
        }

        sb.AppendLine( "return 0; } }" );

        var csharp = sb.ToString();


        /*
         * 
         */
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText( csharp );

        var assemblyName = Path.GetRandomFileName();
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile( typeof( Object ).Assembly.Location ),
            MetadataReference.CreateFromFile( typeof( Enumerable ).Assembly.Location ),
            MetadataReference.CreateFromFile( Assembly.Load( "System.Console" ).Location ),
            MetadataReference.CreateFromFile( Assembly.Load( "System.Runtime" ).Location ),
            MetadataReference.CreateFromFile( Assembly.Load( "System.Collections" ).Location ),
            MetadataReference.CreateFromFile( typeof( List<> ).Assembly.Location ),
            MetadataReference.CreateFromFile( typeof( HttpClient ).Assembly.Location ),
            MetadataReference.CreateFromFile( typeof( IResend ).Assembly.Location ),
        };

        CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions( OutputKind.DynamicallyLinkedLibrary ) );

        using ( var ms = new MemoryStream() )
        {
            EmitResult result = compilation.Emit( ms );

            if ( result.Success == false )
            {
                // handle exceptions
                IEnumerable<Diagnostic> failures = result.Diagnostics.Where( diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error );

                var fg = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write( "err" );
                Console.ForegroundColor = fg;

                Console.WriteLine( " {0} - ln {1}", rel, line );

                foreach ( Diagnostic diagnostic in failures )
                {
                    Console.Error.WriteLine( "{0}: {1}", diagnostic.Id, diagnostic.GetMessage() );
                }

                return Result.Fail;
            }
            else
            {
                var fg = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write( "aok" );
                Console.ForegroundColor = fg;

                Console.WriteLine( " {0} - ln {1}", rel, line );

                return Result.Ok;
            }
        }
    }


    /// <summary />
    private static string GetRelativePath( DirectoryInfo directoryInfo, FileInfo fileInfo )
    {
        Uri directoryUri = new Uri( directoryInfo.FullName + Path.DirectorySeparatorChar );
        Uri fileUri = new Uri( fileInfo.FullName );
        Uri relativeUri = directoryUri.MakeRelativeUri( fileUri );
        string relativePath = Uri.UnescapeDataString( relativeUri.ToString() );

        return relativePath.Replace( '/', Path.DirectorySeparatorChar );
    }
}