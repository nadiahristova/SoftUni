import java.util.*;


public class Prob10_ExtractAllUniqueWords {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line = input.nextLine().toLowerCase();
		
		String[] strArrList1 = line.trim().split("\\W+");		
		Set<String> set = new HashSet<>();
		
		Collections.addAll(set, strArrList1);		
		
		for (String word : set) {
			System.out.print(word + " ");
		}
	}	
}
