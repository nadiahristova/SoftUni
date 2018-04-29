package Exam3;

import java.util.Scanner;

public class _01_DozensOfEggs {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int eggs = 0;
		
		for (int i = 0; i < 7; i++) {
			String line = scan.nextLine().trim();
			String[] input = line.split("\\s+");
			if (input[1].equals("dozens")) {
				eggs += Integer.parseInt(input[0])*12;
			} else eggs += Integer.parseInt(input[0]);
		}
		
		System.out.printf("%d dozens + %d eggs\n",eggs/12,eggs%12);
	}
}
