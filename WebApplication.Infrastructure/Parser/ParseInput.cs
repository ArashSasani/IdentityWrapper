using System;

namespace WebApplication.Infrastructure.Parser
{
    public static class ParseInput
    {
        public static int[] ParseToIntArray(this string items)
        {
            var splits = items.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var itemsIntArray = new int[splits.Length];

            int index = 0;
            for (int i = 0; i < splits.Length; i++)
            {
                if (int.TryParse(splits[i], out int result))
                {
                    itemsIntArray[i] = result;
                }
                else
                {
                    index = i;
                    throw new LogicalException($"cannot parse '{splits[index]}' to int" +
                        $", value passed to the service is not valid integar.");
                }

            }
            return itemsIntArray;
        }
    }
}
