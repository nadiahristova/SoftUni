import java.util.Scanner;

public class Prob5_AngleUnitConverter {
	public static void main(String[] args) {
		Scanner scn = new Scanner(System.in);
		int numCommands = Integer.parseInt(scn.nextLine().trim());
		double[] angle = new double[numCommands];
		String[] kindOfAngle = new String[numCommands];
		for (int i = 0; i < numCommands; i++) {
			angle[i] = scn.nextDouble();
			kindOfAngle[i] = scn.nextLine().trim();
		}
		for (int comm = 0; comm < numCommands; comm++) {
			if (kindOfAngle[comm].equals("deg")) {
				convertToRadian(angle[comm]);
			} else{
				convertToDegrees(angle[comm]);
			}
		}		
	}

	private static void convertToRadian(double deg) {		
		Double radians = deg * Math.PI / 180;
		System.out.printf("%.6f rad\n", radians);
	}	
	private static void convertToDegrees(double rad) {		
		Double degrees = rad * 180 / Math.PI;
		System.out.printf("%.6f deg\n", degrees);
	}
}

