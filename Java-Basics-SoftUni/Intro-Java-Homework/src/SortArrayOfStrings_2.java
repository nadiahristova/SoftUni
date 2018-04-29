import java.util.Arrays;
import java.util.Scanner;

public class SortArrayOfStrings_2 {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);		
		int count = scan.nextInt();		
		String[] arrOfStr = new String[count];
		
		for (int i = 0; i < arrOfStr.length; i++) {
			arrOfStr[i] = scan.next();
		}
		
		Arrays.sort(arrOfStr);		
		
		for(String str: arrOfStr){
			System.out.println(str);
		}
		
		scan.close();
	}

}
