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

        label = new DymoLabel();

        if ( MENU_HANDLER.FindValue( "Type", argument_table, out string[] type_table ) && MENU_HANDLER.FindValue( "Data", argument_table, out string[] data_table ) )
        {
            switch ( type_table[ 0 ].ToLower() )
            {
                case "price":  
                { 
                    label.LoadLabelFromXML( System.IO.File.ReadAllText( "Labels/PriceLabel.xml" ).Replace( "{PRICE}", data_table[ 0 ] ) );
                    DymoPrinter.Instance.PrintLabel( label, "DYMO LabelManager PnP" );
                }
                break;

                case "barcode":
                {  
                    label.LoadLabelFromXML( System.IO.File.ReadAllText( "Labels/BarcodeLabel.xml" ).Replace( "{BARCODE}", data_table[ 0 ] ) );
                    DymoPrinter.Instance.PrintLabel( label, "DYMO LabelManager PnP" );
                }
                break;
            }
        }
        else
        {
            Console.WriteLine( "Requires Type argument with a value of: " + String.Join( ", ", Enum.GetValues( typeof( TYPE ) ).Cast<TYPE[]>().Select( type => type.ToString() ) ) );
        }

    }

    // .. VARIABLES
    
}
