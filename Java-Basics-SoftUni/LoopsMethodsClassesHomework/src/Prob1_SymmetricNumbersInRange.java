import java.util.Scanner;

public class Prob1_SymmetricNumbersInRange {
	public static void main(String[] args) {
		Scanner scn = new Scanner(System.in);
		long startNum = scn.nextLong();
		long endNum = scn.nextLong();
		scn.nextLine();
		for (long givenNum = startNum; givenNum <= endNum; givenNum++) {
			if (isSymmetric(givenNum)) {
				System.out.print(givenNum + " ");
			}
		}		
	}

	private static boolean isSymmetric(long num) {
		boolean isSym = false;		
		int length = Long.toString(num).replace("-",null).length();
		try {
			switch (length) {
			case 1: isSym = true; break;
			case 2: {
				if (num/10 == num%10) {
					isSym = true;
				} else isSym = false;
			} break;
			case 3: {
				if (num/100 == num%10) {
					isSym = true;
				} else isSym = false;
			}break;
			default:throw new RuntimeException();
			}
		} catch (RuntimeException e) {
			System.out.printf("The number %d is out of range([-999...999]). Please try again.\n", num);
			System.exit(0);;
		}		
		return isSym;
	}

}
