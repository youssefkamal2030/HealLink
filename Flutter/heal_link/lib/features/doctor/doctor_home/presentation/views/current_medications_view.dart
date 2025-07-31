import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/current_medications_widget.dart';

import '../../../../../generated/l10n.dart';

class CurrentMedicationsView extends StatelessWidget {
  const CurrentMedicationsView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.only(
          top: 4.0,
          bottom: 16,
          left: 16,
          right: 16,
        ),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).current_medications),
            SizedBox(height: 23),
            Expanded(
              child: ListView.builder(
                padding: EdgeInsets.zero,
                itemBuilder: (context, index) => Padding(
                  padding: const EdgeInsets.only(bottom: 16.0),
                  child: CurrentMedicationsWidget(),
                ),
                itemCount: 5,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
