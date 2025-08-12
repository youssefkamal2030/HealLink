import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/features/doctor/doctor_personal_information/presentation/views/widgets/info_field.dart';
import 'package:heal_link/generated/l10n.dart';

class ProfessionalInfoSection extends StatelessWidget {
  const ProfessionalInfoSection({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      spacing: 8,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          S.of(context).professionalInfo,
          style: AppTextStyles.popins500style16PrimaryColor,
        ),
        InfoField(label: S.of(context).specialization, value: 'Dentist', isEditable: false,),
        InfoField(label: S.of(context).medicalLicenseNumber, value: '1234', isEditable: false,),
        InfoField(
          label: S.of(context).currentWorkplace,
          value: 'Elsafwa clinic',
          img: AppImages.edit,
      isEditable: true,
        ),
      ],
    );
  }
}
