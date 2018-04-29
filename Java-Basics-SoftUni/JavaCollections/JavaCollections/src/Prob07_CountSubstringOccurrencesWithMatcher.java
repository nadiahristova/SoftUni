import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;


public class Prob07_CountSubstringOccurrencesWithMatcher {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String text = input.nextLine().toLowerCase();
		String subString = input.nextLine().toLowerCase();
		
		Pattern pattern = Pattern.compile(subString);
		Matcher match = pattern.matcher(text);
		
		int counter = 0;
		while (match.find()) {
			counter++;
			int currIndex = match.start();
			match.region(currIndex+1, text.length());			
		}
		
		System.out.println(counter);
	}

}
