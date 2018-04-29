import java.util.Scanner;

public class Prob9_PointsInsideTheHouse {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		System.out.print("Please enter the coordinates of the point in question: ");
		Point givenPoint = new Point(input.nextDouble(), input.nextDouble());		
		Point[] leftRec = new Point[]{new Point(12.5, 8.5),new Point(12.5, 13.5),
				new Point(17.5, 13.5), new Point(17.5, 8.5)};
		Point[] rigthRec = new Point[]{new Point(20, 8.5),new Point(20, 13.5),
				new Point(22.5, 13.5), new Point(22.5, 8.5)};
		Point[] triangle = new Point[]{new Point(12.5, 8.5),new Point(22.5, 8.5),
				new Point(17.5, 3.5)};
		boolean isInside = Point.isInsideFig(leftRec,givenPoint) || Point.isInsideFig(rigthRec,givenPoint) ||
				 Point.isInsideFig(triangle,givenPoint);
		
		if (isInside){
			System.out.println("Inside");
		} else {
			System.out.println("Outside");
		}
	}	
}

class Point{
	private double x;
	private double y;
	
	public Point(double x, double y){
		this.x = x;
		this.y = y;
	}
	
	public double getX(){
		return this.x;
	}
	
	public void setX(double x){
		this.x = x;
	}
	
	public double getY(){
		return this.y;
	}
	
	public void setY(double y){
		this.y = y;
	}
	
	public static boolean isInsideFig(Point[] fig, Point pointInQ){
		boolean isIn = true;
		
		for (int i = 0; i < fig.length; i++) {
			int index = i+1;
			if (i == fig.length-1) {
				index =0;
			}
			double findSign = (fig[index].x - fig[i].x)*(pointInQ.y - fig[i].y) -  
					(fig[index].y - fig[i].y)*(pointInQ.x - fig[i].x);
			if (findSign > 0) {
				isIn = false;
			}
		}
		return isIn;
	}
}
