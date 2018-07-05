# Contributing to MonoGame-Test

## Conventions

### Formatting and Naming
* Indent with 4 spaces
* Place brackets on their own line ([Allman style](https://en.wikipedia.org/wiki/Indentation_style#Allman_style))
* Do not leave whitespace at the end of lines!
* Private fields use `_underscoreCamelCase`
* Types, methods, properties, and anything else public facing uses `PascalCase`
* Variable names use `camelCase`

### Other Picky Things
* Explicitly specify access modifiers for all classes and their members
* Use `{ get; set; }` auto-properties where possible
* Prefer public properties over public fields
* Avoid `this` - it should never be neccesary when following naming conventions
* Mark fields only ever assigned in the constructor as `readonly`
* Prefer `var`, especially when the type is readily apparent, Ex. `var t = new Foo()`
