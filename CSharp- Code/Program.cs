using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine(""); //work around for main is not defined error on code compilation 

public partial class JSBridge
{
    private static Employee employee { get; set; }

    [JSImport("globalThis.console.log")]
    internal static partial void Log([JSMarshalAs<JSType.String>] string message);

    [JSExport]
    internal static string GetEmployee()
    {
        string jsonString = JsonSerializer.Serialize(employee, SourceGenerationContext.Default.Employee);
        Log(jsonString);//Logging message log to console
        return jsonString;
    }

    [JSExport]
    internal static void SetEmployee(int _id, string _name, int _age, bool _isActive)
    {
        Log($"{nameof(Employee.EmployeeId)}: {_id}, {nameof(Employee.EmployeeName)}: {_name}, {nameof(Employee.EmployeeAge)}: {_age}, {nameof(Employee.IsActive)}: {_isActive},");
        employee = new Employee() { EmployeeId = _id, EmployeeName = _name, EmployeeAge = _age, IsActive = _isActive };
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Employee))]
internal partial class SourceGenerationContext : JsonSerializerContext
{

}

public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeAge { get; set; }
    public bool IsActive { get; set; }
}