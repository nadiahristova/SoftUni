package exam2;

import java.util.Scanner;

public class _03_Largest3Rectangulars {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		String input =scan.nextLine().replace(" ", "");
		String[] rectang = input.split("[\\[\\]x]+");
		int maxArea = 1;

		for (int i = 1; i < rectang.length - 5; i += 2) {
			int a = Integer.parseInt(rectang[i]);
			int b = Integer.parseInt(rectang[i + 1]);
			int area = a * b;
			for (int j = 0; j < 2; j++) {
				int currA = Integer.parseInt(rectang[i + j * 2 + 2]);
				int currB = Integer.parseInt(rectang[i + j * 2 + 3]);
				area += currA * currB;
			}

			if (area > maxArea) {
				maxArea = area;
			}
		}
		System.out.println(maxArea);
	}
}
