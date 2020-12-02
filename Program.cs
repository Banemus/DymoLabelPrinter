using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DymoSDK.Implementations;

public class PROGRAM
{
    // -- PUBLIC

    // .. TYPES

    public enum TYPE
    {
        Price,
        Barcode
    }

    // .. FUNCTIONS

    public static void Main( 
        string[] argument_table
        )
    {
        DymoLabel
           label;
        string
            label_directory;
         
        label_directory = System.Reflection.Assembly.GetEntryAssembly().Location;

        label_directory = label_directory.Substring( 0, label_directory.LastIndexOf( "\\" ) ).Replace( "\\", "/" ) + "/Labels/";

        label = new DymoLabel();

        if ( MENU_HANDLER.FindValue( "Type", argument_table, out string[] type_table ) && MENU_HANDLER.FindValue( "Data", argument_table, out string[] data_table ) )
        {
            switch ( type_table[ 0 ].ToLower() )
            {
                case "price":  
                {
                    Console.WriteLine( "Contructing Label with price: " + data_table[ 0 ] );
                    label.LoadLabelFromXML( System.IO.File.ReadAllText( label_directory + "PriceLabel.xml" ).Replace( "{PRICE}", data_table[ 0 ] ) );
                }
                break;

                case "barcode":
                {
                    Console.WriteLine( "Contructing Label with barcode data: " + data_table[ 0 ] );
                    label.LoadLabelFromXML( System.IO.File.ReadAllText( label_directory + "BarcodeLabel.xml" ).Replace( "{BARCODE}", data_table[ 0 ] ) );
                }
                break;

                default:
                {
                    Console.WriteLine( "Error type given: '" + type_table[ 0 ].ToLower() + "', quitting the printing." );
                    System.Threading.Thread.Sleep( 5000 );
                    return;
                }
            }

            Console.WriteLine( "Printing" );
            DymoPrinter.Instance.PrintLabel( label, "DYMO LabelManager PnP" );
        }
        else
        {
            Console.WriteLine( "Requires Type argument with a value of: " + String.Join( ", ", Enum.GetValues( typeof( TYPE ) ).Cast<TYPE[]>().Select( type => type.ToString() ) ) );
        }
    }

    // .. VARIABLES
    
}
