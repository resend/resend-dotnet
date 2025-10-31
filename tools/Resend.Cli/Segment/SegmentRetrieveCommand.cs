using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Segment;

/// <summary />
[Command( "get", Description = "Retrieves a segment" )]
public class SegmentRetrieveCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Segment identifier" )]
    [Required]
    public Guid? SegmentId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON array" )]
    public bool InJson { get; set; }


    /// <summary />
    public SegmentRetrieveCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.SegmentRetrieveAsync( this.SegmentId!.Value );
        var segment = res.Content;


        /*
         * 
         */
        if ( this.InJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( segment, jso );

            Console.WriteLine( json );
        }
        else
        {
            // Record
            var record = new Table();
            record.Border = TableBorder.SimpleHeavy;
            record.AddColumn( "Segment Id" );
            record.AddColumn( "Name" );
            record.AddColumn( "Created" );

            record.AddRow(
               new Markup( segment.Id.ToString() ),
               new Markup( segment.Name ),
               new Markup( segment.MomentCreated.ToString() )
            );

            AnsiConsole.Write( record );
        }

        return 0;
    }
}