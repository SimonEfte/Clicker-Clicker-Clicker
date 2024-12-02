// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("OVD72hjkNKVfDDuykFDmgT0gfu5rPEctHaGppCnbCRmN4OTFH513VyAk+t+7GqHyXlTjLE58/7rgNv4wNxLtRYHiS1Sf+cHoFYFzoPMr6P4nxZdhiAbfCBMXLf4Bsxhd4oyzvz+NDi0/AgkGJYlHifgCDg4OCg8MEaYDUJ6kqFUzxdGCLsNbgyvIyW/0TJPEIyEDnxTMliA+fxGxmmwn1QJj9QPYriCLtrr8L0fzt49UT1/g2XJEyS9xXxE+ZUmkeQmHgJ83zV3aLMSgKqo5hWHa8GSowGTekrt3+tmigVGsWTSxUMcKmk3WtLvHjEqIjQ4ADz+NDgUNjQ4OD8/qxe4KiWKGYU0VlYW9BVk6Xm9Bn6ewwHks4R22R6UJnZFFmA0MDg8O");
        private static int[] order = new int[] { 13,3,10,8,11,13,12,9,11,10,13,12,12,13,14 };
        private static int key = 15;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
