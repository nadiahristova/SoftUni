package Exam4;

import java.util.ArrayList;
import java.util.Scanner;

public class _01_Pyramid {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int n = Integer.parseInt(scan.nextLine());
		int num = Integer.MIN_VALUE;
		ArrayList<Integer> answers = new ArrayList<>();
		for (int i = 0; i < n; i++) {
			String[] strArr = scan.nextLine().split("\\s+");
			int currNum = Integer.MAX_VALUE;
			for (int j = 0; j < strArr.length; j++) {
				if (strArr[j].equals("")) {
					continue;
				}
				int bla = Integer.parseInt(strArr[j]);
				if ((bla > num) && currNum > bla ) {
					currNum = bla;
				}				
			}
			if (currNum == Integer.MAX_VALUE) {
				num++;
			} else {
				num = currNum;
				answers.add(num);
			}
		}
		boolean first = true;
		for (int nums:answers) {
			if(!first){
				System.out.print(", ");
			}
			System.out.print(nums);
			first=false;
		}
	}
}

