import java.math.BigDecimal;
import java.util.Arrays;
import java.util.Scanner;

public class ThreeLargestNum {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int n = Integer.parseInt(input.nextLine().trim());
		BigDecimal[] arrBigDec = new BigDecimal[n];
		for (int i = 0; i < n; i++) {
			BigDecimal num = new BigDecimal(input.nextLine().trim());
			arrBigDec[i] = num;
		}
		
		Arrays.sort(arrBigDec);
		
		for (int i = 1; i < 4; i++) {
			if (arrBigDec.length - i >= 0) {
				System.out.println(arrBigDec[arrBigDec.length - i].toPlainString());
			}
		}
	}

}
