import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/prescriptions/presentation/views/widgets/prescription_date.dart';
import 'package:heal_link/generated/l10n.dart';

class Prescriptions extends StatelessWidget {
  const Prescriptions({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).prescriptions),
            SizedBox(height: 24),
            PrescriptionDate(),
          ],
        ),
      ),
    );
  }
}
