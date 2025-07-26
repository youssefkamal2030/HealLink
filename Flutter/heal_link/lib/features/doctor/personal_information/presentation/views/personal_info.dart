import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/widgets/basic_info_section.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/widgets/contact_info_section.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/widgets/professional_info_section.dart';
import 'package:heal_link/generated/l10n.dart';

class PersonalInformationView extends StatelessWidget {
  const PersonalInformationView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).personal_information),
            SizedBox(height: 24),
            BasicInfoSection(),
            SizedBox(height: 24),
            ContactInfoSection(),
            SizedBox(height: 24),
            ProfessionalInfoSection(),
          ],
        ),
      ),
    );
  }
}
