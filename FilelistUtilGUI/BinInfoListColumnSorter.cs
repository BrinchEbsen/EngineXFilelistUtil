using System.Collections;

namespace FilelistUtilGUI
{
    // Code derived from:
    // https://learn.microsoft.com/da-dk/troubleshoot/developer/visualstudio/csharp/language-compilers/sort-listview-by-column

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class BinInfoListColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;

        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer StringCompare;

        /// <summary>
        /// Class constructor. Initializes various elements
        /// </summary>
        public BinInfoListColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            StringCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface. It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = CompareColumn(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        private int CompareColumn(string s1, string s2)
        {
            try
            {
                switch (ColumnToSort)
                {
                    // "Path" column (sort as string)
                    case 1:
                        return StringCompare.Compare(s1, s2);

                    // "HashCode" and "Flags" columns (sort as hex number)
                    case 3:
                    case 5:
                        return
                            uint.Parse(s1, System.Globalization.NumberStyles.HexNumber)
                            .CompareTo(uint.Parse(s2, System.Globalization.NumberStyles.HexNumber));

                    // "Location(s)" column (sort primarily by filelist number, then by hex location number)
                    case 7:
                        {
                            //Only sort by the first of the comma-separated locations
                            string locString1 = s1.Split(',')[0].Trim();
                            string locString2 = s2.Split(',')[0].Trim();

                            //Location string is formatted as LOCATION:FILELISTNUM
                            string[] parts1 = locString1.Split(':');
                            string[] parts2 = locString2.Split(':');

                            //Parse location and filelist
                            uint loc1 = uint.Parse(parts1[0], System.Globalization.NumberStyles.HexNumber);
                            uint num1 = parts1.Length > 1 ? uint.Parse(parts1[1]) : 0;

                            uint loc2 = uint.Parse(parts2[0], System.Globalization.NumberStyles.HexNumber);
                            uint num2 = parts1.Length > 1 ? uint.Parse(parts2[1]) : 0;

                            //Sort first by filelist number, secondarily by location number
                            if (num1 != num2) return num1.CompareTo(num2);
                            else return loc1.CompareTo(loc2);
                        }

                    // All other columns (sort as integer)
                    default:
                        return uint.Parse(s1).CompareTo(uint.Parse(s2));
                }
            } catch { return 0; }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
