import java.util.Scanner;

public class PrintYourHometown {
	public static void main(String[] args) {
		Scanner in  = new Scanner(System.in);
		System.out.print("Please type in your home town: ");
		String homeTown = in.nextLine();
		String country = "Bulgaria";
		in.close();
		System.out.printf("Your home town is %s,%s", homeTown, country);
	}

}