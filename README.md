# Color Sequence Generator
Generates semi-infinite (up to int.MaxValue, default to 1000000 values) or tolerable colors, in constant memory and complexity O(1) for next color. 
        
Main purpose - auto-generation of colors for pie charts and diagrams alike
        
Usage: 
```        
        var csg = ColorSequenceGenerator.ColorSequenceGenerator.Instance;
        var persons = new [] {"John Doe", "Peter Smith", "Jack Daniels"};
        var personsAndColors = persons.Zip(csg.ColorSequence()); // => (John Doe, #3566EE),(Peter Smith, #570B9D),(Jack Daniels, #8DE24A)
```