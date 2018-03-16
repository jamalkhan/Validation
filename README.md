# bleak.Validation
C# Validation framework built around the .NET DataAnnotations

## Getting Started

Install the nuget package.

### Prerequisites

* .NET Standard / Core 2.0 

### Usage

Here is some sample usage.

Your POCO with System.ComponentModel.DataAnnotations
```
    public class TestClass1
    {
        [Required()]
        public string RequiredString { get; set; }

        [Range(-5, 10)]
        public int IntegerRange { get; set; }
    }
```

Your Usage
```
try
{
    var inst = new TestClass1();
    inst.RequiredString = null;
    inst.Validate();
    Assert.IsTrue(false);
}
catch (ValidationException ex)
{
    Console.Write(ex.Message)
}
```

### See Also

Please refer to the github project.

https://github.com/jamalkhan/Validation

### License

This project is licensed with The Unlicense.
https://github.com/jamalkhan/Validation/blob/master/LICENSE