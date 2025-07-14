import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/custom_bottom_nav_bar.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/doctor_home_view_body.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/bottom_nav_scanner_widget.dart';
import 'package:heal_link/features/doctor/doctor_message/presentation/views/doctor_message_view_body.dart';
import 'package:heal_link/features/doctor/doctor_profile/presentation/views/doctor_profile_view_body.dart';
import 'package:heal_link/features/doctor/doctor_scanner/presentation/views/doctor_scanner_view_body.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/views/doctor_subscription_view_body.dart';

class DoctorHomeView extends StatefulWidget {
  const DoctorHomeView({super.key});

  @override
  State<DoctorHomeView> createState() => _DoctorHomeViewState();
}

class _DoctorHomeViewState extends State<DoctorHomeView> {
  int selectedIndex = 0;
  List<Widget> body = [
    DoctorHomeViewBody(),
    DoctorMessageViewBody(),
    DoctorScannerViewBody(),
    DoctorSubscriptionViewBody(),
    DoctorProfileViewBody(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      bottomNavigationBar: CustomBottomNavBar(
        selectedIndex: selectedIndex,
        onTap: (value) {
          selectedIndex = value;
          setState(() {});
        },
      ),
      floatingActionButton: BottomNavScannerWidget(
        onPressed: () {
          selectedIndex = 2;
          setState(() {});
        },
        selectedIndex: selectedIndex,
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerDocked,
      body: body[selectedIndex],
    );
  }
}
