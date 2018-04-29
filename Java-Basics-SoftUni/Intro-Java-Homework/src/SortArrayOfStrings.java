import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Scanner;

public class SortArrayOfStrings {
	public static void main(String[] args) {
		Scanner in = new Scanner(System.in);
		
		int count = in.nextInt();
		String[] arrOfStr = new String[count];
		
		for (int i = 0; i < count; i++) {
		   arrOfStr[i] = in.next();
		}
		in.close();
		
		Arrays.sort(arrOfStr);		
		ArrayList<String> listOfTowns = new ArrayList<String>();
		Collections.addAll(listOfTowns, arrOfStr);
		listOfTowns.forEach(x -> System.out.println(x));
	}

}
