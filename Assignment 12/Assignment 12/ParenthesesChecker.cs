using System;
using System.Collections.Generic;

public class ParenthesesChecker
{
    public static bool IsBalanced(string input)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char ch in input)
        {
            if (ch == '(' || ch == '{' || ch == '[')
            {
                stack.Push(ch);
            }
            else if (ch == ')' || ch == '}' || ch == ']')
            {
                if (stack.Count == 0) return false;

                char top = stack.Pop();

                if ((ch == ')' && top != '(') ||
                    (ch == '}' && top != '{') ||
                    (ch == ']' && top != '['))
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
}
