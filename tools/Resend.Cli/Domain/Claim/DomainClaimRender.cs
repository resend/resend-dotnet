using Spectre.Console;
using System.Text.Json;

namespace Resend.Cli.Domain.Claim;

/// <summary />
public static class DomainClaimRender
{
    /// <summary />
    public static void Write( DomainClaim claim, bool inJson )
    {
        if ( inJson == true )
        {
            var jso = new JsonSerializerOptions() { WriteIndented = true };
            var json = JsonSerializer.Serialize( claim, jso );

            Console.WriteLine( json );
            return;
        }


        var head = new Table();
        head.Border = TableBorder.SimpleHeavy;
        head.AddColumn( "Claim Id" );
        head.AddColumn( "Domain Id" );
        head.AddColumn( "Name" );
        head.AddColumn( "Region" );
        head.AddColumn( "Status" );

        head.AddRow(
            new Markup( claim.Id.ToString() ),
            new Markup( claim.DomainId?.ToString() ?? "" ),
            new Markup( claim.Name ),
            new Markup( claim.Region?.ToString() ?? "" ),
            new Markup( claim.Status.ToString() )
            );

        AnsiConsole.Write( head );


        if ( claim.Record != null )
        {
            var table = new Table();
            table.Border = TableBorder.SimpleHeavy;
            table.AddColumn( "Type" );
            table.AddColumn( "Name" );
            table.AddColumn( "TTL" );
            table.AddColumn( "Value" );

            table.AddRow(
                new Markup( claim.Record.RecordType ),
                new Markup( claim.Record.Name ),
                new Markup( claim.Record.TimeToLive ),
                new Markup( claim.Record.Value )
                );

            AnsiConsole.Write( table );
        }
    }
}
