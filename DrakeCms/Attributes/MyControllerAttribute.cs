namespace DrakeCms.Attributes;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MyControllerAttribute : Attribute 
    {
    public MyControllerAttribute(string tagName) {
        
        this.TagName = tagName;
    }
    public string TagName { get; set; }
}
