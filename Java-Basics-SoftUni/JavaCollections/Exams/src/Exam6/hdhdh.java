package Exam6;

import java.util.ArrayList;
import java.util.Scanner;

public class hdhdh {
	public static void main(String[] args) {
		ArrayList<Integer> list = new ArrayList<>();
		Scanner scan = new Scanner(System.in);
		int[] nums = new int[3];
		int d = scan.nextInt();
		scan.nextLine();
		String line = scan.nextLine();
		boolean isIt = false;
		int maxSum = Integer.MIN_VALUE;
		
		while (!line.equals("End")) {
			list.add(Integer.parseInt(line));
			line = scan.nextLine();
		}
		
		for (int i = 0; i < list.size(); i++) {
			for (int f = i + 1; f < list.size(); f++) {
				for (int j = f + 1; j < list.size(); j++) {
					int a = list.get(i);
					int b = list.get(f);
					int c = list.get(j);
					int sum = a + b + c;
					if (sum % d == 0) {
						if (sum > maxSum) {
							nums[0] = a;
							nums[1] = b;
							nums[2] = c;
							maxSum = sum;
						}
						isIt = true;
					}
				}
			}
		}
		
		if (!isIt) {
			System.out.println("No");
		} else {
			System.out.printf("(%d + %d + %d) %% %d = 0", nums[0], nums[1],
					nums[2], d);
		}
	}
}
