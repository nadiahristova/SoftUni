import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;


public class Prob08_ExtractEmails {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String text = input.nextLine();
		String emailRegex = "((\\p{Alnum}+[\\.\\-_]?\\p{Alnum}+)@[[a-z]+[//.//-]?[a-z]+]+\\.[a-z]+)";
 		Pattern pattern =Pattern.compile(emailRegex);
		Matcher match = pattern.matcher(text);
		while (match.find()) {
			int currIndex = match.start();
			System.out.println(match.group());
			match.region(currIndex+match.group().length(), text.length());
		}
	}

}
