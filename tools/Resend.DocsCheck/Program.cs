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

        foreach ( var f in dir.GetFiles( "*.mdx", SearchOption.AllDirectories ) )
        {
            await FileCheck( dir, f );
        }

        return 0;
    }


    /// <summary />
    private async Task FileCheck( DirectoryInfo root, FileInfo f )
    {
        var rel = GetRelativePath( root, f );
        var mdx = await File.ReadAllTextAsync( f.FullName );


        /*
         * 
         */
        var startIx = mdx.IndexOf( "```csharp" );

        if ( startIx < 0 )
            return;

        var endIx = mdx.IndexOf( "```", startIx + 4 );


        var fragment = mdx.Substring( startIx, endIx - startIx + 3 );


        /*
         * 
         */
        var lines = fragment.Split( "\n" );

        var sb = new StringBuilder();
        sb.AppendLine( "using System;" );
        sb.AppendLine( "using System.Threading.Tasks;" );

        foreach ( var l in lines )
            if ( l.StartsWith( "using " ) == true )
                sb.AppendLine( l );

        sb.AppendLine();
        sb.AppendLine( "public class Program {" );
        sb.AppendLine( "public static async Task<int> Main( string[] args ) {" );

        foreach ( var l in lines.Skip( 1 ).Take( lines.Count() - 2 ) )
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
            MetadataReference.CreateFromFile( Assembly.Load( "System.Console" ).Location ),
            MetadataReference.CreateFromFile( Assembly.Load( "System.Runtime" ).Location ),
            MetadataReference.CreateFromFile( Assembly.Load( "System.Collections" ).Location ),
            MetadataReference.CreateFromFile( typeof( List<> ).Assembly.Location ),
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

                Console.Write( " " );
                Console.WriteLine( rel );

                foreach ( Diagnostic diagnostic in failures )
                {
                    Console.Error.WriteLine( "{0}: {1}", diagnostic.Id, diagnostic.GetMessage() );
                }
            }
            else
            {
                var fg = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write( "aok" );
                Console.ForegroundColor = fg;

                Console.Write( " " );
                Console.WriteLine( rel );
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