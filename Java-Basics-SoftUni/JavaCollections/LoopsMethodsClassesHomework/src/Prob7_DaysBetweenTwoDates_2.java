import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Scanner;
import java.util.concurrent.TimeUnit;

public class Prob7_DaysBetweenTwoDates_2 {
	public static void main(String[] args) {
		Scanner input  = new Scanner(System.in);
		System.out.print("Enter the first date: ");
		String firstDateStr = input.nextLine().trim();
		System.out.print("Enter the second date: ");
		String secondDateStr = input.nextLine().trim();	
		Date firstDate = null; 
		Date secondDate=null;		
		SimpleDateFormat formatter = new SimpleDateFormat("dd-MM-yyyy");	
		
		try {
			firstDate =  formatter.parse(firstDateStr);
			secondDate = formatter.parse(secondDateStr);
		} catch (ParseException e) {
			System.out.println("Invalid input.");
		}
		
		long diffInMilicecs = secondDate.getTime() - firstDate.getTime(); 
		int days = (int) TimeUnit.MILLISECONDS.toDays(diffInMilicecs); 
		System.out.println("Diffrence is " + days + " day(s).");
	}
}
