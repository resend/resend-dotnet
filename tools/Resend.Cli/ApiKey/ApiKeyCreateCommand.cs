﻿using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.ApiKey;

/// <summary />
[Command( "create", Description = "Create an API key" )]
public class ApiKeyCreateCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "API key name" )]
    [Required]
    public string KeyName { get; set; } = default!;

    /// <summary />
    [Option( "-p|--permission", CommandOptionType.SingleValue, Description = "Key permissions" )]
    public Permission? Permission { get; set; }

    /// <summary />
    [Option( "-d|--domain", CommandOptionType.SingleValue, Description = "Domain identifier" )]
    public Guid? DomainId { get; set; }


    /// <summary />
    public ApiKeyCreateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.ApiKeyCreateAsync( this.KeyName, this.Permission, this.DomainId );
        var apiKey = res.Content;


        /*
         * 
         */
        Console.WriteLine( apiKey.Id );
        Console.WriteLine( apiKey.Token );

        return 0;
    }
}
