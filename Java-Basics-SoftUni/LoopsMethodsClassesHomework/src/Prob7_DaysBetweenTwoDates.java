import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.time.temporal.ChronoUnit;
import java.util.Scanner;

public class Prob7_DaysBetweenTwoDates {
	public static void main(String[] args) {
		Scanner input  = new Scanner(System.in);
		System.out.print("Enter the first date: ");
		String firstDateStr = input.nextLine().trim();
		System.out.print("Enter the second date: ");
		String secondDateStr = input.nextLine().trim();
	
		LocalDate firstDate =  null;
		LocalDate secondDate =  null;		
		
		try {
			DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd-MM-yyyy");
			firstDate =  LocalDate.parse(firstDateStr,formatter);
			secondDate =  LocalDate.parse(secondDateStr,formatter);		
		} catch (DateTimeParseException e) {
			System.out.println("Invalid input.");
		}
		int days = (int) ChronoUnit.DAYS.between(firstDate, secondDate);
		////Period betweenDays = Period.between(firstDate, secondDate); - var 1	
		////int days = betweenDays.getDays(); - var 1
		System.out.println("Diffrence is " + days + " day(s).");
	}

}
