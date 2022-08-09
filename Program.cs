using IronXL;
using System.Net.Http.Json;
using WildrerriesParser.Model;


using (var client = new HttpClient())
{    
    string filePathKeys = "Keys.txt";
    List<string> keys = new List<string>();
    keys = File.ReadAllLines(filePathKeys).ToList();

    var workbook = WorkBook.Create(ExcelFileFormat.XLSX);
    workbook.Metadata.Title = "parsed.xlsx";

    foreach (var key in keys)
    {
        WorkSheet sheet = workbook.CreateWorkSheet($"{key}");
        sheet[$"A{1}"].Value ="Title";
        sheet[$"B{1}"].Value = "Brand";
        sheet[$"C{1}"].Value = "Id";
        sheet[$"D{1}"].Value = "Feedbacks";
        sheet[$"E{1}"].Value = "Price";
        var endpoint = new Uri($"https://search.wb.ru/exactmatch/sng/common/v4/search?appType=1&couponsGeo=12,7,3,21&curr=&dest=12358386,12358403,-70563,-8139704&emp=0&lang=ru&locale=by&page=1&pricemarginCoeff=1&query={key}&reg=0&regions=68,83,4,80,33,70,82,86,30,69,22,66,31,40,1,48&resultset=catalog&sort=popular&spp=0&suppressSpellcheck=false");
        var result = await client.GetAsync(endpoint);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Response is not successful");
        }
        
        var json = await result.Content.ReadFromJsonAsync<WbResponse>();
        int i = 2;
        foreach (var product in json.Data.Products)
        {            
            sheet[$"A{i}"].Value = product.Name;
            sheet[$"B{i}"].Value = product.Brand;
            sheet[$"C{i}"].Value = product.Id;
            sheet[$"D{i}"].Value = product.Feedbacks;
            sheet[$"E{i++}"].Value = product.PriceU;
        }        
    }
    workbook.SaveAs("parsed.xlsx");
    Console.WriteLine("Parsing executed!");

}

Console.ReadLine();