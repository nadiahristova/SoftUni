package exam5;

import java.util.Scanner;

public class _02_PossTriangle {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		String line = scan.nextLine();
		boolean boo = false;
		while (!line.equals("End")) {
			String[] nums = line.split(" ");
			for (int i = 0; i < nums.length; i++) {
				for (int j = 0; j < nums.length; j++) {
					for (int j2 = 0; j2 < nums.length; j2++) {
						if (i!=j && j!=j2 && j2!=i) {
							double a = Double.parseDouble(nums[i]);
							double b = Double.parseDouble(nums[j]);
							double c = Double.parseDouble(nums[j2]);
							if ((a+b) > c && a < c && b < c && a<=b) {
								System.out.printf("%.2f+%.2f>%.2f\n",a,b,c);
								boo = true;
							}
						}
					}
				}
			}
			line = scan.nextLine();
		}
		if (!boo) {
			System.out.println("No");
		}
	}
}
