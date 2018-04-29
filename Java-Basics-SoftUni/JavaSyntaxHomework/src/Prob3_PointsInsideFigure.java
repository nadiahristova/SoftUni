import java.util.Scanner;

public class Prob3_PointsInsideFigure {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		System.out.print("Enter the coordinates of a point: ");
		float x = input.nextFloat();
		float y = input.nextFloat();
		
		boolean isInBigRec = x >= 12.5 && x<=22.5 && y>=6 && y<=13.5;
		boolean isInEmptyRec = x > 17.5 && x<20 && y>8.5 && y<13.5;
		
		if (isInBigRec ^ isInEmptyRec) {
			System.out.println("Inside");
		} else {
			System.out.println("Outside");
		}		
	}
}
