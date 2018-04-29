import java.util.Scanner;

public class Probl13_Durts {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String[] centerStr = input.nextLine().trim().split("\\s+");
		int cenX = Integer.parseInt(centerStr[0]);
		int cenY = Integer.parseInt(centerStr[1]);
		double range = input.nextInt();
		int n = input.nextInt();
		input.nextLine();
		int[][] dots = new int[n][2];
		for (int i = 0; i < n; i++){
			double dotX = input.nextInt();
			double dotY = input.nextInt();
			boolean isInCross = isInRec(cenX,cenY,range,dotX,dotY)|| isInRec(cenY,cenX,range,dotY,dotX);
			if (isInCross) {
				System.out.println("yes");
			} else System.out.println("no");
		}		
	}

	private static boolean isInRec(int x, int y, double r, double dotX, double dotY) {
		boolean isIn = (((dotX>=x-r) && (dotX<=x+r)) && ((dotY>=y-r/2) && (dotY<=y+r/2)));
		return isIn;
	}
}
