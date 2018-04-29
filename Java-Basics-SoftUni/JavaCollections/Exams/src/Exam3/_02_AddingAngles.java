package Exam3;

import java.util.Scanner;

public class _02_AddingAngles {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int n = Integer.parseInt(scan.nextLine());
		String[] anglesStr = scan.nextLine().split("\\s+");
		boolean formCircle = false;
		
		for (int a1 = 0; a1 < anglesStr.length-2; a1++) {
			for (int a2 = a1+1; a2 < anglesStr.length-1; a2++) {
				for (int a3 = a2+1; a3 < anglesStr.length; a3++) {
					//if (a1 != a2 && a2!= a3 && a1!=a3) {
						int angle1 = Integer.parseInt(anglesStr[a1]);
						int angle2 = Integer.parseInt(anglesStr[a2]);
						int angle3= Integer.parseInt(anglesStr[a3]);
						int angleSum = angle1 + angle2 + angle3;
						
						if (angleSum % 360 == 0) {
							System.out.printf("%d + %d + %d = %d degrees\n", angle1, angle2, angle3, angleSum);
							formCircle = true;
						}
					//}
				}
			}
		}
		
		if (!formCircle) {
			System.out.println("No");
		}
	}
}
