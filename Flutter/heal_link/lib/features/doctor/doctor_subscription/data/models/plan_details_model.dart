class PlanDetailsModel {
  final String month;
  final String description;
  final String price;

  PlanDetailsModel({
    required this.month,
    required this.description,
    required this.price,
  });
}


final List<PlanDetailsModel> planDetailsList = [
  PlanDetailsModel(
    month: 'Monthly',
    description: 'Figma ipsum component variant main layer.',
    price: '200 EGP /mo',
  ),
  PlanDetailsModel(
    month: 'Weekly',
    description: 'Figma ipsum component variant main layer.',
    price: '550 EGP /3mo',
  ),
  PlanDetailsModel(
    month: 'One-time ',
    description: 'Figma ipsum component variant main layer.',
    price: '2000 EGP /12mo',
  ),
];