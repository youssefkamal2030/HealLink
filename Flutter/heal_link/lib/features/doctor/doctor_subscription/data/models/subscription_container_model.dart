class SubscriptionContainerModel {
  final String title ; 
 final String totalSubscription;
  SubscriptionContainerModel(this.title, {
    required this.totalSubscription,
  });
 

}



final List <SubscriptionContainerModel> subscriptionContainerModels = [
  SubscriptionContainerModel("Total Subscriptions", totalSubscription: "40"),
  SubscriptionContainerModel("New this month", totalSubscription: "40"),
  SubscriptionContainerModel("Revenue this month", totalSubscription: "450 EGP"),
];