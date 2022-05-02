namespace AGLS.Models {
    public class InventoryItem {
        public long Id { get; set; }
        public string? Barcode { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        
        /// <summary>
        /// Checks for null/whitespace strings and a negative price.
        /// </summary>
        public bool Validate() {
            return !string.IsNullOrWhiteSpace(Barcode) && !string.IsNullOrWhiteSpace(Name) && Price > 0;
        }
    }
}
