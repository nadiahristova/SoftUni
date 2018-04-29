import java.util.Arrays;
import java.util.Scanner;


public class Prob01_SorArrayOfNumbers {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int n = input.nextInt();
		int[] arrNum = new int[n];
		
		for (int i = 0; i < n; i++) {
			arrNum[i] = input.nextInt();
		}
		input.nextLine();
		Arrays.sort(arrNum);
		
		for(int num:arrNum){
			System.out.print(num+ " ");
		}
	}

}
