import 'package:flutter/material.dart';

import '../../../../../../core/utils/app_styles.dart';
import '../../../../../../generated/l10n.dart';
import 'add_prescription_text_form_field.dart';

class AddPrescriptionBodyTop extends StatelessWidget {
  const AddPrescriptionBodyTop({
    super.key,
    required this.patientNameController,
    required this.dateController,
    required this.ageController,
  });

  final TextEditingController patientNameController;
  final TextEditingController dateController;
  final TextEditingController ageController;

  @override
  Widget build(BuildContext context) {
    return Column(
      spacing: 8,
      children: [
        Row(
          children: [
            Text(
              '${S.of(context).name}:',
              style: AppTextStyles.popins500style14LightBlackColor.copyWith(
                color: Colors.black,
              ),
            ),
            Expanded(
              child: AddPrescriptionTextFormField(
                textInputType: TextInputType.text,
                hintText:
                    '_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ',
                textEditingController: patientNameController,
                validator: (value) {
                  if (value.toString().isEmpty) {
                    return 'enter medicine name';
                  }
                  return null;
                },
              ),
            ),
          ],
        ),
        Row(
          children: [
            Text(
              '${S.of(context).date}:',
              style: AppTextStyles.popins500style14LightBlackColor.copyWith(
                color: Colors.black,
              ),
            ),
            Expanded(
              child: AddPrescriptionTextFormField(
                validator: (value) {
                  if (value.toString().isEmpty) {
                    return 'enter medicine name';
                  }
                  return null;
                },
                textInputType: TextInputType.datetime,
                hintText: '_ _ _ / _ _ _ / _ _ _ _ _',
                textEditingController: dateController
              ),
            ),
            SizedBox(width: 3),
            Text(
              '${S.of(context).age}:',
              style: AppTextStyles.popins500style14LightBlackColor.copyWith(
                color: Colors.black,
              ),
            ),
            SizedBox(
              width: 108,
              child: AddPrescriptionTextFormField(
                validator: (value) {
                  if (value.toString().isEmpty) {
                    return 'enter medicine name';
                  }
                  return null;
                },
                textInputType: TextInputType.number,
                hintText: '_ _ _ _ _ _ _ _ _ _ _ ',
                textEditingController: ageController
              ),
            ),
          ],
        ),
      ],
    );
  }
}
