# Code convention

Unity & C# code convention. Every piece of code is typed in the English language.

- [Namespaces](#namespaces)
    - [Importing namespaces](#importing-namespaces)
- [Classes](#classes)
- [Functions](#functions)
- [Variables](#variables)
- [Structs](#structs)
- [Enums](#enums)
- [If statement](#if-statements)
- [Loops](#loops)
- [Styling](#styling)
    - [Namespaces](#namespaces-style)
    - [Bracket Placement](#bracket-placement)
    - [If statement and loop](#if-statement-and-loop)
    - [Regions](#regions)

------
### Namespaces
The namespace name is written in PascalCasing. Every class, scriptableObject and struct needs to be inside of a namespace.
```cs
namespace ExampleNamespace
{
    public class ExampleScript : MonoBehaviour
    {
        
    }
}
```
```cs
using UnityEngine;

namespace ExampleNamespace.ScriptableObjects
{
    public class ExampleScriptableObject : ScriptableObject
    {
        
    }
}
```
```cs
using Systems;

namespace ExampleNamespace
{
    public struct ExampleStruct
    {
        
    }
}
```

#### Importing namespaces
When using namespaces we put the default namespaces first then a white space followed by our namespaces.

```cs
using System;
using System.Collections;
using UnityEngine;

using FrameWork;
using FrameWork.Enums;
using FrameWork.Extensions;
using Player;
```

------
### Classes
The Class name is written in PascalCasing. I suggest using the 'sealed' and 'abstract' keywords to minimize confusion.
```cs
public sealed class ExampleScript : MonoBehaviour
{

}

public abstract class BaseExampleScript : MonoBehaviour
{

}

public class NonBaseExampleScript : BaseExampleScript
{

}
```

Class members should be grouped into sections:

- Variables
- Unity Methods (ag. Awake, OnEnable, OnDisable, OnDestroy)
- Public Methods
- Protected Methods
- Private Methods

Within each of these groups order by access:
- public
- serializedFields
- protected
- private

------
### Functions
All functions and events perform some form of action, whether its getting info, calculating data, or causing something to explode. Therefore, all functions should **start with verbs**. They should be worded in the present tense whenever possible. They should also have some context as to what they are doing.

The Function name is written in PascalCasing.
```cs
private void ExampleFunction()
{
    
}
```

**Access modifiers** are always written with functions.
```cs
void ExampleFunction()
{
    Debug.Log("Not allowed");
}

private void ExampleFunction()
{
    Debug.Log("I'm a private function.");
}

protected void ExampleFunction()
{
    Debug.Log("I'm a protected function.");
}

public void ExampleFunction()
{
    Debug.Log("I'm a public function.");
}
```
------
### Variables
A variable is almost always private. If you need the value make a getter for it. This is also why serialized have a '_' exception.

**Access modifiers** are always written with variables.
```cs
// Allowed
private int _variableExample0;
protected int p_variableExample1;
public int variableExample2;

// Not allowed
int _variableExample3;
```

**Private variable** names always start with an '_' (Except when serialized) after which it is written in camelCasing.
```cs
private Object _variableExample;

[SerializeField] private Object secondVariableExample;
```

**Public variable** names are written in camelCasing. If not a number, char, string or bool, it needs to has the Tooltip attribute.
```cs
[Tooltip("Explaination of this varible.")] public Object variableExample;
```

**Readonly variable** names are written the same as public variables so in camelCasing.
```cs
public readonly Object variableExample;
```

**Constant variable** names are written in FULL_CAPITALS with snake_casing.
```cs
public const int EXAMPLE_CONSTANT_VALUE;
```

**Protected variable** names always start with 'p_' after which it is written in camelCasing.
```cs
protected int p_variableExample;
```

**Temporary variables** inside of an function always need to be written out and are written in camelCasing.
```cs
private void ExampleFunction()
{
    float temporaryFloat = 1f;
    int temporaryInt = 1;
    double temporaryDouble = 1.00;
}
```

**Temporary constants** inside of an function always need to be written out and are written in FULL_CAPITALS with snake_casing.
```cs
private void ExampleFunction()
{
    const float TEMPORARY_FLOAT = 1f;
    const int TEMPORARY_INT = 1;
    const double TEMPORARY_DOUBLE = 1.00;
}
```

**Property** names are written in PascalCasing.
```cs
public int ExampleInteger
{
    get => _exampleInterger;
    set 
    {
        if (value < 0)
            _exampleInterger = 0;
    }
}
```

------
### Structs
The struct name is written in PascalCasing and everything inside the struct follows the usual code conventions.
```cs
public struct ExampleStruct
{
    public double x;
    public double y;
}
```

------
### Enums
The enum name is written in PascalCasing while the constants are in FULL_CAPITALS with snake_casing.
```cs
enum ExampleEnum
{
    FIRST_CONSTANT,
    SECOND_CONSTANT
}
```
Always have the default type at the top.

```cs
enum CookedState
{
    NONE,
    RAW,
    COOKED
}
```

------
### If statements
When there is only 1 line of code after an if statement it comes right after it and same with the else.
```cs
if (_exampleBoolean)
    ExampleFunction();
else
    SeccondExampleFunction();

if (_exampleBoolean)
    return;
```

If either the if or the else in the statement contains multiple lines of code, the if and the else do not need brackets both.
```cs
if (_exampleBoolean)
    ExampleFunction();
else
{
    SeccondExampleFunction();
    ThirdExampleFunction();
}
```

When the condition has multiple conditions, make new lines for it.
```cs
// bad example
if (_exampleBoolean && 0 == 0 || true)
    ExampleFunction();

// good example
if (_exampleBoolean
    && 0 == 0
    || true)
    ExampleFunction();

// also good example
bool canBeCalled = _exampleBoolean && 0 == 0 || true;
if (canBeCallled)
    ExampleFunction();
```

------
### Loops
For better performance (even very small) we make the length it's own (local)variable.
```cs
int listLength = _exampleList.Length; // naming this int `l` is fine

for (int i = 0; i < listLength; i++)
{

}
```

------
### Styling
Code style is a personal preference. It is needed for a group project, so here is a style that we use.

Want to look at a [class with good code style](https://github.com/bas-boop/BaguetteTime/blob/main/Assets/Scripts/Framework/Gameplay/Farming/Grain.cs)?

#### Namespaces style
There needs to be line between the namespaces in use and the current namespace. There should also be a line between default namespaces and the projects namespaces.
```cs
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Framework {}
```

```cs
using UnityEngine;

using Framework.Enums;
using Framework.Extensions;
using Framework.Extensions.Attributes;

namespace Framework.Gameplay {}
```

#### Bracket placement
A good usage of brackets:
```cs
namespace ExampleNamespace
{
    public class ExampleScript : MonoBehaviour
    {
        private void ExampleMethod()
        {

        }
    }
}
```
A bad usage of brackets:
```cs
namespace ExampleNamespace{
    public class ExampleScript : MonoBehaviour{
        private void ExampleMethod(){


}       }


}
```

#### If statement and loop
Around an if and loop there needs to be an empty line above and below.
```cs
float exampleFloat;

if (_exampleBoolean)
    exampleFloat = 1;

ExampleMethod(exampleFloat);
```
```cs
int listLength = _exampleList.Length;

for (int i = 0; i < listLength; i++)
{
    Debug.LogError($"Item {listLenght} is {_exampleList[listLength].name}.");
}

ExampleMethod();
```

#### Regions
A region has a line between its content.
```cs
#region Private variables

private int _targetAmount;
private ExampleComponent _system;
private ExampleStruct _currentStruct;

#endregion
```
```cs
#region Public functions

public void ExampleMethod()
{
    Debug.Log("Example")
}

#endregion
```

#### Functions
Have a line in between functions. This is how it should be done:
```cs
/// <summary>
/// Function description.
/// </summary>
/// <param name="parameter">Parameter value to pass.</param>
/// <returns>What the function return.</returns>
public int ExampleFunction(string parameter)  
{
    Return 0;
}

private void ExampleFunction()  
{
    Debug.log("I am example!");
}
```
Not like this:

```cs
/// <summary>
/// Function description.
/// </summary>
/// <param name="parameter">Parameter value to pass.</param>
/// <returns>What the function return.</returns>
public int ExampleFunction(string parameter)  
{
    Return 0;
}
private void ExampleFunction()  
{
    Debug.log("I am example!");
}
```
