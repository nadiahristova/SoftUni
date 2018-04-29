package exam5;

import java.util.Scanner;

public class _01_MirroNums {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int n = Integer.parseInt(input.nextLine());
		boolean boo = false;
		String line = input.nextLine();
		String[] nums = line.split("\\s+");
		for (int i = 0; i < nums.length-1; i++) {
			String currNum = nums[i];
			for (int j = i+1; j < nums.length; j++) {
				String reversedNum = new StringBuilder(currNum).reverse().toString();
				if (nums[j].equals(reversedNum)) {
					System.out.println(currNum + "<!>" + reversedNum);
					boo = true;
				}
			}
		}
		if (!boo) {
			System.out.println("No");
		}
	}

//	private static String reverse(String string) {
//		//char[] ch = new char[string.length()];
//		String str = new StringBuilder(string).reverse().toString();
//		//Arrays.asList(string.toCharArray()).toCharArray();
//		return str;
//	}

}
