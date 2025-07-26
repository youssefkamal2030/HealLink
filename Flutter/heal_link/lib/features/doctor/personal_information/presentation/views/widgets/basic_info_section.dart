import 'package:flutter/cupertino.dart';
import 'package:heal_link/generated/l10n.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/widgets/info_field.dart';

class BasicInfoSection extends StatelessWidget {
  const BasicInfoSection({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      spacing: 8,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          S.of(context).basicInfo,
          style: AppTextStyles.popins500style16PrimaryColor,
        ),
        InfoField(
          label: S.of(context).fullName,
          value: 'Yousry Ahmed',
          isEditable: false,
        ),
        InfoField(label: S.of(context).gender, value: 'Man', isEditable: false),
        InfoField(
          label: S.of(context).nationality,
          value: 'Egyptian',
          isEditable: false,
        ),
      ],
    );
  }
}
