namespace ASP_Project.Areas.Identity.Data.Models;

public enum Color
{
    None,
    Multicolor,
    White,
    Black,
    Gray,
    Beige,
    Cream,
    Red,
    Burgundy,
    Purple,
    Pink,
    Blue,
    Azure,
    Yellow,
    Orange,
    Brown,
    Green,
    Turquoise,
    Khaki,
    Ecru,
    Gold,
    Silver,
}
public enum ProcessorName
{
	Apple,
	Exynos,
	Kirin,
	Qualcomm,
	MediaTek,
	Unisoc
}
public enum OperatingSystemEnum
{
	IOS,
	Android,
	EMUI,
	HarmonyOS  
}
public enum Producer
{
	Apple,
	Xiomi,
	Samsung,
	Nokia,
	OnePlus,
	HUAWEI,
	Google,
	Honor,
	Infinix,
	Itel,
	Motorola,
	Vivo,
	Tecno,
	TCL
}

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
    public string InternalMemory { get; set; }
    public string RAM { get; set; }
    public uint Quantity { get; set; }
    public short? Discount { get; set; } 
	
    public Color Color { get; set; }
	public Producer Producer { get; set; }
	public ProcessorName ProcessorName { get; set; }
	public OperatingSystemEnum OperatingSystemEnum { get; set; }

	public bool? NFC { get; set; }
	public string BatteryCapacity { get; set; }
	public int NumberOfSIMCards { get; set; }
	public int NuclearNumber { get; set; }
	//add raiting 
    
	public ShoppingCartItem ShoppingCartItem { get; set; } //test i dont know would it work//

	public int CategoryId { get; set; }
	public Category Category { get; set; } 

    public List<ProductImage> ProductImages { get; set; }
    

}