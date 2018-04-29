import java.util.Scanner;

public class Prob2_TriangleArea {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		int[][] points = new int[3][2];
		int[] positions = new int[]{0,1,2,0,1};
		for (int i = 0; i < 3; i++) {
			System.out.print("Enter the coordinates of the " + (i+1) + " point: ");
			points[i][0] = input.nextInt();
			points[i][1] = input.nextInt();	
			System.out.println();
		}
		
		float sum =0f;
		for (int i = 0; i < points.length; i++) {
			sum = sum + points[positions[i]][0] * (points[positions[i+1]][1]-points[positions[i+2]][1]);
		}
		
		float area = Math.abs(sum/2);
		System.out.printf("The area of the triangle is: %.2f", area);
	}

}
