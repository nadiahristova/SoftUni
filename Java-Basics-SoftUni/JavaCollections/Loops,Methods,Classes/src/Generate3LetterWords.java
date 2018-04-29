import java.util.Scanner;

public class Generate3LetterWords {

	public static void main(String[] args) {

		Scanner scanner = new Scanner(System.in);
		char[] charArr = scanner.next().toCharArray();
		for (char first : charArr) {
			for (char second : charArr) {
				for (char third : charArr) {
					System.out.printf("%c%c%c ", first, second, third);
				}
			}
		}
	}
}
