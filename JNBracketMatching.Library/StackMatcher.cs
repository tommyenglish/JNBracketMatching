namespace JNBracketMatching.Library;

/// <summary>
/// A bracket matcher that uses a stack to keep track of the brackets
/// by pushing onto the stack when an opening bracket is found and
/// popping from the stack when a closing bracket is found.
/// The stack should be empty at the end of the input string to be valid.
/// This runs through the input string once with one short circuit case - max O(n).
/// </summary>
public class StackMatcher : IBracketMatcher
{
    public bool IsValid(string input)
    {
        var stack = new Stack<int>();
        foreach (var c in input)
        {
            if (c == '<')
            {
                stack.Push(1);
            }
            else if (c == '>')
            {
                // Assumes a closing bracket found before an opening bracket is invalid.
                if (stack.Count == 0) 
                {
                    return false;
                }
                stack.Pop();
            }
        }

        return stack.Count == 0;
    }
}
