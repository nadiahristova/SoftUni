import java.util.Arrays;
import java.util.Scanner;


public class SortArrayOfStrings {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int len = scan.nextInt();
		String[] city = new String[len];
		
		for (int i = 0; i < city.length; i++) {
			city[i] = scan.next();
		}
		
		Arrays.sort(city);
		
		for (int i = 0; i < city.length; i++) {
			System.out.println(city[i]);
		}
		scan.close();
	}
}
