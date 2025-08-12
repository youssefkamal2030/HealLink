import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/doctor_radiology/presentation/views/widgets/custom_tab_bar.dart';
import 'package:heal_link/features/doctor/doctor_radiology/presentation/views/widgets/radiology_widget.dart';
import 'package:heal_link/generated/l10n.dart';

class RadiologyView extends StatelessWidget {
  const RadiologyView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).radiology),
            SizedBox(height: 24),
            CustomTabBar(
              tabs: [
                'Neurology',
                'Orthopedics',
                'Cardiology',
                'Dentistry',
                'Ophthalmology',
              ],
              onTabSelected: (index) {},
            ),
            SizedBox(height: 24),
            RadiologyWidget(),
          ],
        ),
      ),
    );
  }
}
