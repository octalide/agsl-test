using AGLS.Models;

namespace AGLS.Utilities
{
    public static class Data
    {
        /// <summary>
        /// Loads an inventory csv file into a list of InventoryItem objects
        /// <param name="path">The path to the csv file</param>
        /// <param name="skip">If false, format exceptions will be thrown for invalid items.</param>
        /// </summary>
        public static List<InventoryItem> LoadInventoryCSV(string path, bool skip = false, bool verbose = false)
        {
            Console.WriteLine($"Reading inventory data from {path}");
            var inventory = new List<InventoryItem>();
            var lineNumber = 0;
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    lineNumber++;

                    // if line is blank or starts with '//', skip it
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    {
                        continue;
                    }

                    if (verbose) {
                        Console.WriteLine($"  {lineNumber}: \"{line}\"");
                    }

                    var values = line.Split(',');
                    if (values.Length == 3)
                    {
                        var item = new InventoryItem();

                        // validate that the barcode is numeric and add it to the item
                        var barcode = values[0].Trim();
                        if (long.TryParse(barcode, out var barcodeValue) && barcode.Length <= 13)
                        {
                            item.Barcode = barcode;
                        }
                        else
                        {
                            var msg = $"Invalid barcode: \"{barcode}\"";

                            if (skip)
                            {
                                Console.WriteLine(msg);
                                continue;
                            }
                            throw new FormatException(msg);
                        }

                        // validate that the name is not null and is under 128 characters long
                        var name = values[1].Trim();
                        if (!string.IsNullOrWhiteSpace(name) && name.Length <= 128)
                        {
                            item.Name = name;
                        }
                        else
                        {
                            var msg = $"Invalid name: \"{name}\"";

                            if (skip)
                            {
                                Console.WriteLine(msg);
                                continue;
                            }
                            throw new FormatException(msg);
                        }

                        // validate that the price is correctly formatted and is above zero
                        var price = values[2].Trim();
                        if (float.TryParse(price, out var priceValue) && priceValue > 0)
                        {
                            item.Price = priceValue;
                        }
                        else
                        {
                            var msg = $"Invalid price: \"{price}\"";

                            if (skip)
                            {
                                Console.WriteLine(msg);
                                continue;
                            }
                            throw new FormatException(msg);
                        }

                        // add the inventory item to the list
                        inventory.Add(item);
                    }
                    else
                    {
                        var msg = $"Invalid line: \"{line}\"";

                        if (skip)
                        {
                            Console.WriteLine(msg);
                            continue;
                        }
                        throw new FormatException(msg);
                    }
                }
            }

            return inventory;
        }
    }
}
