import { TripExpenditure } from "./trip-expenditure";
import { TripParticulars } from "./trip-particulars";
import { TripInformation } from "./trip-tripInformation";

export class TripData {
    tripInfo: TripInformation;
    tripParticulars?: TripParticulars[];
    tripExpenditures?: TripExpenditure[];
  
    constructor() {
      this.tripInfo = new TripInformation(); // Initialize tripInfo to avoid undefined errors
      this.tripParticulars = [];
      this.tripExpenditures = [];
    }
  }
  