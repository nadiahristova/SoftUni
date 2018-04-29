import java.util.Scanner;
import java.util.regex.Pattern;

public class Prob07_CountSubstringOccurrences {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line = input.nextLine();
		String subStrToMatch = input.nextLine();
		String[] words = line.trim().split("\\W+");
		int count = 0;// var1
		String regex = ".*"+subStrToMatch.toLowerCase()+".*"; // var2
		int countMatch = 0;//var2
		for (int i = 0; i < words.length; i++) {
			String word = words[i].toLowerCase();
			while (word.length() >= subStrToMatch.length()) {
				if (word.contains(subStrToMatch.toLowerCase())) { //var1
					count++;
				}			
				if (Pattern.matches(regex, word)) {//var2
					countMatch++;
				}
				word = word.substring(1, word.length());
			}				
		}
		System.out.println(count);//var1
		System.out.println(countMatch);//var2
	}

}
