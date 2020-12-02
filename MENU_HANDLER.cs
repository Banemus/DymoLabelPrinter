using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MENU_HANDLER
{
    // -- PUBLIC

    // .. FUNCTIONS
    
    public static string[] CreateArgumentTable( 
        string commandLine 
        )
    {
        string[]
            argument_table;

        argument_table = commandLine.Split( new string[]{ " -" }, StringSplitOptions.None );

        return argument_table;
    }

    // .. INQUIRIES

    public static bool FindValue(
        string argument_name,
        string[] argument_table,
        out int[] value_table
        )
    {
        string[]
            result_table;

        if ( !FindValue( argument_name, argument_table, out result_table ) )
        {
            value_table = new int[ 0 ];
            return false;
        }

        value_table = new int[ result_table.Length ];

        for ( int index = 0; index < result_table.Length; ++index )
        {
            if ( int.TryParse( result_table[ index ], out int result ) )
            {
                value_table[ index ] = result;
            }
            else
            {
                Console.WriteLine( "Cannot convert :'" + result_table[ index ] + "' to an integer." );
                return false;
            }
        }

        return true;
    }

    // ~~

    public static bool FindValue(
        string argument_name,
        string[] argument_table,
        out decimal[] value_table
        )
    {
        string[]
            result_table;

        if ( !FindValue( argument_name, argument_table, out result_table ) )
        {
            value_table = new decimal[ 0 ];
            return false;
        }

        value_table = new decimal[ result_table.Length ];

        for ( int index = 0; index < result_table.Length; ++index )
        {
            if ( decimal.TryParse( result_table[ index ], out decimal result ) )
            {
                value_table[ index ] = result;
            }
            else
            {
                Console.WriteLine( "Cannot convert :'" + result_table[ index ] + "' to an integer." );
                return false;
            }
        }

        return true;
    }

    // ~~

    public static bool FindValue(
        string argument_name,
        string[] argument_table,
        out string[] value
        )
    {
        List<string>
            result_table;

        result_table = new List<string>();

        for ( int index = 0; index < argument_table.Length; ++index )
        {
            string
                argument;
            int
                equal_index;

            equal_index = argument_table[ index ].IndexOf( "=" );

            if ( equal_index == -1 )
            {
                continue;
            }

            argument = argument_table[ index ].Substring( 0, equal_index ).Replace( " ", "" ).Replace( "-", "" );

            if ( argument.ToLower() == argument_name.ToLower() )
            {
                result_table.Add( argument_table[ index ].Substring( equal_index + 1 ) );
            }
        }

        value = result_table.ToArray();

        return value.Length != 0;
    }

    // ~~

    public static bool HasArgument(
        string argument_name,
        string[] argument_table
        )
    {
        string
            lowered_argument_name;

        lowered_argument_name = argument_name.ToLower();

        for ( int index = 0; index < argument_table.Length; ++index )
        {
            if ( argument_table[ index ].Replace( " ","" ).Replace( "-", "" ).ToLower() == lowered_argument_name )
            {
                return true;
            }
        }

        return false;
    }
}